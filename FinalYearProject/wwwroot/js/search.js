document.addEventListener('DOMContentLoaded', function() {
    const searchInput = document.querySelector('.input-group input[type="search"]');
    if (!searchInput) return;

    // Set ARIA attributes for search input
    searchInput.setAttribute('role', 'combobox');
    searchInput.setAttribute('aria-controls', 'search-results');
    searchInput.setAttribute('aria-autocomplete', 'list');

    const searchResults = document.getElementById('search-results');
    const liveRegion = searchInput.parentElement.querySelector('[aria-live="polite"]');

    let debounceTimer;
    let currentFocusIndex = -1;

    // Function to handle keyboard navigation
    function handleKeyboardNavigation(e) {
        const items = searchResults.querySelectorAll('.search-result-item');
        if (!items.length) return;

        if (items.length) {
            items.forEach(item => {
                item.addEventListener('keydown', handleKeyboardNavigation);
            });
        }

        if (e.key === 'ArrowDown' || e.key === 'ArrowUp') {
            e.preventDefault();
            
            // Update focus index
            if (e.key === 'ArrowDown') {
                if (currentFocusIndex < items.length - 1) {
                    currentFocusIndex += 1;
                }
            } else {
                if (currentFocusIndex > 0) {
                    currentFocusIndex -= 1;
                }
            }

            // Remove focus from all items
            items.forEach(item => {
                item.classList.remove('focused');
                item.setAttribute('aria-selected', 'false');
            });

            // Set focus on current item
            const currentItem = items[currentFocusIndex];
            currentItem.classList.add('focused');
            currentItem.setAttribute('aria-selected', 'true');
            currentItem.focus();

            // Update ARIA
            searchInput.setAttribute('aria-activedescendant', `search-result-${currentFocusIndex}`);
        } else if (e.key === 'Enter' && currentFocusIndex >= 0) {
            e.preventDefault();
            items[currentFocusIndex].click();
        } else if (e.key === 'Escape') {
            searchResults.style.display = 'none';
            searchInput.setAttribute('aria-expanded', 'false');
            currentFocusIndex = -1;
            searchInput.focus();
        }
    }

    searchInput.addEventListener('keydown', handleKeyboardNavigation);

    searchInput.addEventListener('input', function() {
        clearTimeout(debounceTimer);
        const query = this.value.trim();
        currentFocusIndex = -1; // Reset focus index on new search
        
        if (query === '') {
            searchResults.style.display = 'none';
            searchInput.setAttribute('aria-expanded', 'false');
            return;
        }

        debounceTimer = setTimeout(async () => {
            try {
                const response = await fetch(`/Search/LiveSearch?query=${encodeURIComponent(query)}`);
                if (!response.ok) throw new Error('Search failed');
                
                const bakes = await response.json();
                
                searchResults.innerHTML = '';
                if (bakes.length > 0) {
                    bakes.forEach((bake, index) => {
                        const li = document.createElement('li');
                        li.setAttribute('role', 'presentation');
                        
                        const resultItem = document.createElement('a');
                        resultItem.href = `/Bakes/Details/${bake.id}`;
                        resultItem.className = 'search-result-item';
                        resultItem.setAttribute('role', 'option');
                        resultItem.setAttribute('id', `search-result-${index}`);
                        resultItem.setAttribute('tabindex', '-1');
                        resultItem.setAttribute('aria-selected', 'false');
                        resultItem.innerHTML = `
                            <img src="/images/${bake.name}.jpg" alt="${bake.alttext}" class="search-result-image">
                            <div class="search-result-info">
                                <div class="search-result-name">${bake.name}</div>
                                <div class="search-result-price">£${bake.price.toFixed(2)}</div>
                            </div>
                        `;
                        li.appendChild(resultItem);
                        searchResults.appendChild(li);
                    });
                    searchResults.style.display = 'block';
                    searchInput.setAttribute('aria-expanded', 'true');
                    liveRegion.textContent = `${bakes.length} products found`;
                } else {
                    searchResults.style.display = 'none';
                    searchInput.setAttribute('aria-expanded', 'false');
                    liveRegion.textContent = 'No products found';
                }
            } catch (error) {
                console.error('Search error:', error);
                liveRegion.textContent = 'Search failed. Please try again.';
            }
        }, 300);
    });

    // Hide search results when clicking outside
    document.addEventListener('click', function(e) {
        if (!searchResults.contains(e.target) && e.target !== searchInput) {
            searchResults.style.display = 'none';
            searchInput.setAttribute('aria-expanded', 'false');
            currentFocusIndex = -1;
        }
    });
});