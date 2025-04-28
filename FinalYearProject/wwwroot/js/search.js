document.addEventListener('DOMContentLoaded', function() {
    const searchInput = document.querySelector('.input-group input[type="search"]');
    if (!searchInput) return;

    const searchResults = document.createElement('div');
    searchResults.className = 'search-results';
    searchInput.parentElement.appendChild(searchResults);

    let debounceTimer;

    searchInput.addEventListener('input', function() {
        clearTimeout(debounceTimer);
        const query = this.value.trim();
        
        if (query === '') {
            searchResults.style.display = 'none';
            return;
        }

        debounceTimer = setTimeout(async () => {
            try {
                const response = await fetch(`/Search/LiveSearch?query=${encodeURIComponent(query)}`);
                if (!response.ok) throw new Error('Search failed');
                
                const bakes = await response.json();
                
                searchResults.innerHTML = '';
                if (bakes.length > 0) {
                    bakes.forEach(bake => {
                        const resultItem = document.createElement('a');
                        resultItem.href = `/Bakes/Details/${bake.id}`;
                        resultItem.className = 'search-result-item';
                        resultItem.innerHTML = `
                            <img src="/images/${bake.name}.jpg" alt="${bake.name}" class="search-result-image">
                            <div class="search-result-info">
                                <div class="search-result-name">${bake.name}</div>
                                <div class="search-result-price">£${bake.price.toFixed(2)}</div>
                            </div>
                        `;
                        searchResults.appendChild(resultItem);
                    });
                    searchResults.style.display = 'block';
                } else {
                    searchResults.style.display = 'none';
                }
            } catch (error) {
                console.error('Search error:', error);
            }
        }, 300);
    });

    // Hide search results when clicking outside
    document.addEventListener('click', function(e) {
        if (!searchResults.contains(e.target) && e.target !== searchInput) {
            searchResults.style.display = 'none';
        }
    });
});