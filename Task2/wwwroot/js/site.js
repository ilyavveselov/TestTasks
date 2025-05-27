function fillSelectors(referenceArray, selectorData, selectedSelector) {
    let returnHTML = '';
    referenceArray.forEach((item) => {
        if (selectorData.includes(item)) {
            const activeClass = (item === selectedSelector) ? 'active' : '';
            returnHTML += `<span class="size ${activeClass}">${item}</span>`;
        }
        else {
            returnHTML += `<span class="size disable">${item}</span>`;
        }
    })
    return returnHTML;
}

function renderPizzaCard(pizza, avaiableSizes, avaiableTypes) {
    const activeSize = pizza.sizes.includes(avaiableSizes[0])
        ? avaiableSizes[0]
        : pizza.sizes.find(size => avaiableSizes.includes(size));
    let sizesHTML = fillSelectors(avaiableSizes, pizza.sizes, activeSize);
    let typesHTML = '';
    if (pizza.types.length > 0) {
        const activeType = pizza.types.includes(avaiableTypes[0])
            ? avaiableTypes[0]
            : pizza.types.find(type => avaiableTypes.includes(type))
        typesHTML = fillSelectors(avaiableTypes, pizza.types, activeType)
    }
    const hitHTML = pizza.isHit
        ? '<span class="pizza-card-hit">HIT</span>'
        : '';
    let halfOptionHTML = '';
    if (pizza.showHalf) {
        pizza.canHalf
            ? halfOptionHTML += '<span class="pizza-card-half-selector active">1/2</span>'
            : halfOptionHTML += '<span class="pizza-card-half-selector disable">1/2</span>'
    }
    const card = `
            <div class="pizza-card">
                <div class="pizza-card-header">
                    ${hitHTML}
                    <img class="pizza-card-img"
                         src="${pizza.image}" />
                    <span class="pizza-card-name">${pizza.name}</span>
                </div>
                <div class="pizza-card-description">
                    ${pizza.description}
                </div>
                <div class="pizza-card-selectors">
                    <div class="size-selector">
                        ${sizesHTML}
                    </div>
                    <div class="type-selector">
                        ${typesHTML}
                    </div>
                </div>
                <div class="pizza-card-info">
                    <span class="pizza-card-price">${pizza.price} Р</span>
                    <span class="pizza-card-weight">${pizza.weight} гр</span>
                    ${halfOptionHTML}
                </div>
                <button class="in-cart-button">В корзину</button>
            </div>
        `;
    return card;
}