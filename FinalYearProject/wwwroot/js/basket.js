document.addEventListener('DOMContentLoaded', function() {
    document.querySelectorAll('.delete-button').forEach(button => {
        button.addEventListener('click', function() {
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
    const buttybutton = document.querySelector('.add-to-cart');
    if (buttybutton) {
        buttybutton.addEventListener('click', function () {
            const bakeId = document.getElementById('bakeId').value;

            fetch(`/Basket/AddToBasket?bakeId=${bakeId}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        showNotification(data.message);
                    } else {
                        showNotification(data.message, 'error');
                    }
                })
                .catch(error => {
                    console.error('Error:', error);
                    showNotification('Failed to add to basket', true);
                });

        });
    }
}

function showNotification(message, isError = false) {
    const notification = document.createElement('div');
    notification.className = `notification ${isError ? 'error' : 'success'}`;
    notification.textContent = message;
    document.body.appendChild(notification);

    setTimeout(() => {
        notification.classList.add('fade-out');
        setTimeout(() => {
            notification.remove();
        }, 300);
    }, 2000);
}