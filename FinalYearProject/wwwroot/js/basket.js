document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.delete-button').forEach(button => {
        button.addEventListener('click', function () {
            const bakeId = this.getAttribute('data-bake-id');
            if (!bakeId) return;

            fetch('/Basket/DeleteFromBasket', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ bakeId: parseInt(bakeId) })
            })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        const basketItem = this.closest('.basket-item');
                        basketItem.remove();

                        // Update total amount
                        const totalAmount = data.basket.bakes.reduce((sum, bake) => sum + bake.price, 0);
                        document.querySelector('.total-amount span:last-child').textContent =
                            `£${totalAmount.toFixed(2)}`;

                        // Show empty basket message if no items left
                        if (data.basket.bakes.length === 0) {
                            const basketContainer = document.querySelector('.basket-container');
                            basketContainer.innerHTML = `
                            <h1 class="basket-title">Your Basket</h1>
                            <div class="empty-basket">
                                <p>Your basket is empty</p>
                                <a href="/Search" class="continue-shopping">Continue Shopping</a>
                            </div>
                        `;
                        }
                    } else {
                        alert('Failed to delete item from basket');
                    }
                })
                .catch(error => {
                    console.error('Error:', error);
                    alert('An error occurred while deleting the item');
                });
        });
    });


});

document.addEventListener('DOMContentLoaded', function () {
    const basketForm = document.querySelector('.basket-form');
    if (basketForm) {
        basketForm.addEventListener('submit', function (e) {
            e.preventDefault();
            const formData = new FormData(basketForm);
            const currentUrl = window.location.href;

            fetch(basketForm.action, {
                method: 'POST',
                body: formData
            })
                .then(response => {
                    if (response.redirected) {
                        const redirectUrl = response.url;

                        if (redirectUrl.includes('/User/Login')) {
                            // Add the current URL as the return URL
                            const returnUrl = encodeURIComponent(window.location.href);
                            window.location.href = redirectUrl + (redirectUrl.includes('?') ? '&' : '?') + 'returnUrl=' + returnUrl;
                            return null;
                        } else {
                            showToastNotification('Item added to basket!');

                            if (redirectUrl === currentUrl) {
                                return null;
                            }

                            sessionStorage.setItem('basketToast', 'Item added to basket!');

                            window.location.href = redirectUrl;
                            return null;
                        }
                    }

                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    }

                    const contentType = response.headers.get('content-type');
                    if (contentType && contentType.includes('application/json')) {
                        return response.json();
                    }
                    return null;
                })
                .then(data => {
                    if (data === null) return;

                    if (data.success) {
                        showToastNotification('Item added to basket!');
                    } else {
                        if (data.message && data.message.includes('not logged in')) {
                            const returnUrl = encodeURIComponent(window.location.href);
                            window.location.href = '/User/Login?returnUrl=' + returnUrl;
                        } else {
                            throw new Error(data.message || 'Failed to add item to basket');
                        }
                    }
                })
                .catch(error => {
                    console.error('Error:', error);
                    showToastNotification(error.message || 'Failed to add to basket', true);
                });
        });
    }

    const toastMessage = sessionStorage.getItem('basketToast');
    if (toastMessage) {
        showToastNotification(toastMessage);
        sessionStorage.removeItem('basketToast');
    }
});

document.querySelector('.btn.btn-primary')?.addEventListener('click', function (event) {
    event.preventDefault();
    window.history.back();
});

function showToastNotification(message, isError = false) {
    // Remove any existing toasts first
    const existingToasts = document.querySelectorAll('.toast-notification');
    existingToasts.forEach(toast => toast.remove());

    const toast = document.createElement('div');
    toast.className = 'toast-notification';
    if (isError) {
        toast.style.backgroundColor = '#dc3545';
    }

    // Position at top middle
    toast.style.bottom = 'auto';
    toast.style.right = 'auto';
    toast.style.top = '20px';
    toast.style.left = '50%';
    toast.style.transform = 'translateX(-50%)';

    toast.textContent = message;
    document.body.appendChild(toast);

    // Force reflow to trigger transition
    void toast.offsetWidth;
    toast.classList.add('show');

    setTimeout(() => {
        toast.classList.remove('show');
        setTimeout(() => toast.remove(), 500);
    }, 3000);
}