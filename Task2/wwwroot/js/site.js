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

function getAvaiableSizes() { return [30, 40, 60] };
function getAvaiableTypes() { return ["Традиционное", "Толстое"] };

function getActiveSelectors(pizza) {
    const avaiableSizes = getAvaiableSizes();
    const avaiableTypes = getAvaiableTypes();

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
    return { sizes: sizesHTML, types: typesHTML }
}
function renderPizzaCard(pizza) {
    const selectorsHTML = getActiveSelectors(pizza);
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
                            ${pizza.name}
                        </a>
                    </span>
                </div>
                <div class="pizza-card-description">
                    ${pizza.description}
                </div>
                <div class="pizza-card-selectors">
                    <div class="size-selector">
                        ${selectorsHTML.sizes}
                    </div>
                    <div class="type-selector">
                        ${selectorsHTML.types}
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
function renderModalPizzaCard(pizza) {
    pizza = JSON.parse(pizza);
    const selectorsHTML = getActiveSelectors(pizza);
    const hitHTML = pizza.isHit
        ? '<span class="pizza-card-hit">HIT</span>'
        : '';
    let halfOptionHTML = '';
    if (pizza.showHalf) {
        pizza.canHalf
            ? halfOptionHTML += '<span class="pizza-card-half-selector active">1/2</span>'
            : halfOptionHTML += '<span class="pizza-card-half-selector disable">1/2</span>'
    }
    card = `
    <div class="modal-content">
        <div class="modal-header">
            <h5 class="modal-title">${pizza.name}</h5>
        </div>
        <div class="modal-body">
            <div class="pizza-card-modal" data-id=${pizza.id}>
                <div class="pizza-img-container">
                    <img class="pizza-card-img" src="${pizza.image}" alt="${pizza.name}" />
                </div>
                <div class="pizza-body-modal">
                    <div class="pizza-card-header">
                        ${hitHTML}
                        <span class="pizza-card-name">${pizza.name}</span>
                    </div>
                    <div class="pizza-card-description">
                        ${pizza.description}
                    </div>

                    <div class="pizza-card-selectors">
                        <div class="size-selector">
                           ${selectorsHTML.sizes}
                        </div>
                        <div class="type-selector">
                            ${selectorsHTML.types}
                        </div>
                    </div>

                    <div class="pizza-card-info">
                        <span class="pizza-card-price">${pizza.price} Р</span>
                        <span class="pizza-card-weight">${pizza.weight} гр</span>
                        <span class="pizza-card-half">
                            ${halfOptionHTML}
                        </span>
                    </div>
                    <button class="in-cart-button">В корзину</button>
                </div>
            </div>
        </div>
    </div>
    `;
    return card;
}
function setRedirectToModalOnAllPizzaImages() {
    $(document).on('click', '.pizza-card-img, .pizza-card-name', function () {
        const parent = $(this).closest('.pizza-card');
        const pizzaId = parent.data('id');
        if (pizzaId) {
            $.ajax({
                url: "/Home/GetPizzaByIdJSON",
                type: "GET",
                data: { id: pizzaId },
                dataType: "html",
                success: function (pizza) {
                    const pizzaHtml = renderModalPizzaCard(pizza);
                    $('#pizzaModal .modal-dialog').html(pizzaHtml);
                    $('#pizzaModal').modal('show');
                },
                error: function (data) {
                    $('#pizzaModal .modal-dialog').html('<div class="alert alert-danger">Ошибка загрузки данных</div>');
                    $('#pizzaModal').modal('show');
                }
            });

            $('#pizzaModal').on('hidden.bs.modal', function () {
                $(this).find('.modal-dialog').empty();
            });
        }
        else {
            console.log(parent, "не имеет атрибут data-id");
        }
    });
}