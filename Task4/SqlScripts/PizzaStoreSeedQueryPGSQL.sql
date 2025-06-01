CREATE TABLE "Pizzas" (
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(100) NOT NULL UNIQUE,
    "Image" TEXT NOT NULL,
    "Description" VARCHAR(500) NOT NULL,
    "Price" INT NOT NULL CHECK ("Price" >= 100),
    "Weight" INT NOT NULL DEFAULT 100,
    "ShowHalf" BOOLEAN NOT NULL DEFAULT FALSE,
    "CanHalf" BOOLEAN NOT NULL DEFAULT FALSE,
    "IsHit" BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TABLE "Sizes" (
    "Id" SERIAL PRIMARY KEY,
    "Value" INT NOT NULL UNIQUE
);

CREATE TABLE "DoughTypes" (
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE "PizzaSizes" (
    "PizzaId" INT NOT NULL,
    "SizeId" INT NOT NULL,
    PRIMARY KEY ("PizzaId", "SizeId"),
    FOREIGN KEY ("PizzaId") REFERENCES "Pizzas"("Id") ON DELETE CASCADE,
    FOREIGN KEY ("SizeId") REFERENCES "Sizes"("Id") ON DELETE CASCADE
);

CREATE TABLE "PizzaDoughTypes" (
    "PizzaId" INT NOT NULL,
    "DoughTypeId" INT NOT NULL,
    PRIMARY KEY ("PizzaId", "DoughTypeId"),
    FOREIGN KEY ("PizzaId") REFERENCES "Pizzas"("Id") ON DELETE CASCADE,
    FOREIGN KEY ("DoughTypeId") REFERENCES "DoughTypes"("Id") ON DELETE CASCADE
);

INSERT INTO "Sizes" ("Value") VALUES (30), (40), (60);

INSERT INTO "DoughTypes" ("Name") VALUES 
('Традиционное'), 
('Толстое');

INSERT INTO "Pizzas" ("Name", "Image", "Description", "Price", "Weight", "ShowHalf", "CanHalf", "IsHit")
VALUES
('Capriccio', '/images/pizzas/capriccio_1.webp', 'Сыр моцарелла, Соус "Барбекю", Соус "Кальяри", Пепперони, Овощи гриль, Бекон, Ветчина Томаты черри, Шампиньоны', 500, 800, TRUE, FALSE, TRUE),
('XXXL', '/images/pizzas/xxxl_1.webp', 'Сыр моцарелла, Соус "1000 островов", Куриный рулет, Ветчина, Колбаски охотничьи, Бекон, Сервелат, Огурцы маринованные, Томаты черри, Маслины, Лук маринованный', 880, 1440, TRUE, TRUE, FALSE),
('4 вкуса', '/images/pizzas/4_vkusa_1.webp', 'Соус "1000 островов", Сыр моцарелла, Рулет куриный, Ветчина, Пепперони, Сыр пармезан, Шампиньоны, Томаты свежие, Маслины/оливки', 540, 540, FALSE, FALSE, FALSE),
('Амазонка', '/images/pizzas/amazonka_1.webp', 'Соус "Томатный", Сыр моцарелла, Куриная грудка, Брокколи, Огурцы маринованные, Перец болгарский, Шампиньоны, Томаты черри, Маслины, Лук маринованный', 550, 600, TRUE, FALSE, FALSE),
('БананZZа', '/images/pizzas/bananzza_1.webp', 'Соус "1000 островов", Сыр моцарелла, Рулет куриный, Ветчина, Пепперони, Сыр пармезан, Шампиньоны, Томаты свежие, Маслины/оливки', 530, 520, TRUE, FALSE, FALSE),
('Барбекю', '/images/pizzas/barbeq_1.webp', 'Соус "Томатный", Сыр моцарелла, Ветчина, Бекон, Пепперони, Соус "Барбекю", Томаты, Перец болгарский, Лук маринованный', 570, 590, TRUE, TRUE, FALSE),
('Гавайская', '/images/pizzas/hawai_1.webp', 'Ветчина, Соус "Гавайский", Сыр моцарелла, Ананас, Перец болгарский', 530, 550, TRUE, FALSE, FALSE),
('Гавайская Premium', '/images/pizzas/hawai_premium_1.webp', 'Соус "Гавайский", Сыр моцарелла, Ананас, Ветчина, Куриный рулет, Кукуруза, Перец болгарский', 550, 590, TRUE, FALSE, FALSE);

INSERT INTO "PizzaSizes" ("PizzaId", "SizeId") VALUES
(1, 2), (1, 3),
(2, 1), (2, 2),
(3, 1), (3, 2), (3, 3),
(4, 1), (4, 2), (4, 3),
(5, 1), (5, 2), (5, 3),
(6, 1), (6, 2), (6, 3),
(7, 1), (7, 2), (7, 3),
(8, 1), (8, 2), (8, 3);

INSERT INTO "PizzaDoughTypes" ("PizzaId", "DoughTypeId") VALUES
(1, 1), (1, 2),
(2, 1), (2, 2),
(3, 1), (3, 2),
(4, 1), (4, 2),
(6, 1), (6, 2),
(7, 1), (7, 2),
(8, 1), (8, 2);
