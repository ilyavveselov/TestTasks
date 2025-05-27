function fillSelectors(referenceArray, selectorData, selectedSelector, isSizeSelector) {
    let returnHTML = '';
    referenceArray.forEach((item) => {
        if (selectorData.includes(item)) {
            const activeClass = (item === selectedSelector) ? 'active' : '';
            returnHTML += `<span class="size ${activeClass}">${item} ${isSizeSelector ? 'см' : ''}</span>`;
        }
        else {
            returnHTML += `<span class="size disable">${item} ${isSizeSelector ? 'см' : ''}</span>`;;
        }
    })
    return returnHTML;
}

function renderPizzaCard(pizza, avaiableSizes, avaiableTypes) {
    const activeSize = pizza.sizes.includes(avaiableSizes[0])
        ? avaiableSizes[0]
        : pizza.sizes.find(size => avaiableSizes.includes(size));
    let sizesHTML = fillSelectors(avaiableSizes, pizza.sizes, activeSize, true);
    let typesHTML = '';
    if (pizza.types.length > 0) {
        const activeType = pizza.types.includes(avaiableTypes[0])
            ? avaiableTypes[0]
            : pizza.types.find(type => avaiableTypes.includes(type))
        typesHTML = fillSelectors(avaiableTypes, pizza.types, activeType, false)
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
            <div class="pizza-card" data-id=${pizza.id}>
                <div class="pizza-card-header">
                    ${hitHTML}
                    <img class="pizza-card-img"
                         src="${pizza.image}" alt=${pizza.name}/>
                    <span class="pizza-card-name">
                        <a href="/Home/Details/${pizza.id}">
                            ${pizza.name}
                        </a>
                    </span>
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
                    <span class="pizza-card-half">${halfOptionHTML}</span>
                </div>
                <button class="in-cart-button">В корзину</button>
            </div>
        `;
    return card;
}
function setRedirectToDetailsOnAllPizzaImages() {
    $(document).on('click', '.pizza-card-img', function () {
        const parent = $(this).closest('.pizza-card');
        const pizzaId = parent.data('id');
        if (pizzaId) {
            window.location.href = `/Home/Details/${pizzaId}`;
        }
        else {
            console.log(parent, "не имеет атрибут data-id");
        }
    });
}