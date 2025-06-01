USE PizzasStore

CREATE TABLE Pizzas(
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL UNIQUE,
    Image NVARCHAR(MAX) NOT NULL,
    Description NVARCHAR(500) NOT NULL,
    Price INT NOT NULL CHECK (Price >= 100),
    Weight INT NOT NULL DEFAULT 100,
    ShowHalf BIT NOT NULL DEFAULT 0,
    CanHalf BIT NOT NULL DEFAULT 0,
    IsHit BIT NOT NULL DEFAULT 0,
);

CREATE TABLE Sizes(
    Id INT IDENTITY PRIMARY KEY,
	Value INT NOT NULL UNIQUE
);

CREATE TABLE DoughTypes(
    Id INT IDENTITY PRIMARY KEY,
	Name NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE PizzaSizes(
    PizzaId INT NOT NULL,
    SizeId INT NOT NULL,
    PRIMARY KEY (PizzaId, SizeId),
    FOREIGN KEY (PizzaId) REFERENCES Pizzas(Id) ON DELETE CASCADE,
    FOREIGN KEY (SizeId) REFERENCES Sizes(Id) ON DELETE CASCADE
);

CREATE TABLE PizzaDoughTypes(
    PizzaId INT NOT NULL,
    DoughTypeId INT NOT NULL,
    PRIMARY KEY (PizzaId, DoughTypeId),
    FOREIGN KEY (PizzaId) REFERENCES Pizzas(Id) ON DELETE CASCADE,
    FOREIGN KEY (DoughTypeId) REFERENCES DoughTypes(Id) ON DELETE CASCADE
);

INSERT INTO Sizes (Value) VALUES (30), (40), (60);
INSERT INTO DoughTypes (Name) VALUES (N'Традиционное'), (N'Толстое');
INSERT INTO Pizzas (Name, Image, Description, Price, Weight, ShowHalf, CanHalf, IsHit)
VALUES
(N'Capriccio', '/images/pizzas/capriccio_1.webp', N'Сыр моцарелла, Соус "Барбекю", Соус "Кальяри", Пепперони, Овощи гриль, Бекон, Ветчина Томаты черри, Шампиньоны', 500, 800, 1, 0, 1),
(N'XXXL', '/images/pizzas/xxxl_1.webp', N'Сыр моцарелла, Соус "1000 островов", Куриный рулет, Ветчина, Колбаски охотничьи, Бекон, Сервелат, Огурцы маринованные, Томаты черри, Маслины, Лук маринованный', 880, 1440, 1, 1, 0),
(N'4 вкуса', '/images/pizzas/4_vkusa_1.webp', N'Соус "1000 островов", Сыр моцарелла, Рулет куриный, Ветчина, Пепперони, Сыр пармезан, Шампиньоны, Томаты свежие, Маслины/оливки', 540, 540, 0, 0, 0),
(N'Амазонка', '/images/pizzas/amazonka_1.webp', N'Соус "Томатный", Сыр моцарелла, Куриная грудка, Брокколи, Огурцы маринованные, Перец болгарский, Шампиньоны, Томаты черри, Маслины, Лук маринованный', 550, 600, 1, 0, 0),
(N'БананZZа', '/images/pizzas/bananzza_1.webp', N'Соус "1000 островов", Сыр моцарелла, Рулет куриный, Ветчина, Пепперони, Сыр пармезан, Шампиньоны, Томаты свежие, Маслины/оливки', 530, 520, 1, 0, 0),
(N'Барбекю', '/images/pizzas/barbeq_1.webp', N'Соус "Томатный", Сыр моцарелла, Ветчина, Бекон, Пепперони, Соус "Барбекю", Томаты, Перец болгарский, Лук маринованный', 570, 590, 1, 1, 0),
(N'Гавайская', '/images/pizzas/hawai_1.webp', N'Ветчина, Соус "Гавайский", Сыр моцарелла, Ананас, Перец болгарский', 530, 550, 1, 0, 0),
(N'Гавайская Premium', '/images/pizzas/hawai_premium_1.webp', N'Соус "Гавайский", Сыр моцарелла, Ананас, Ветчина, Куриный рулет, Кукуруза, Перец болгарский', 550, 590, 1, 0, 0);
INSERT INTO PizzaSizes (PizzaId, SizeId) VALUES (1, 2), (1, 3);
INSERT INTO PizzaSizes (PizzaId, SizeId) VALUES (2, 1), (2, 2);
INSERT INTO PizzaSizes (PizzaId, SizeId) VALUES (3, 1), (3, 2), (3, 3);
INSERT INTO PizzaSizes (PizzaId, SizeId) VALUES (4, 1), (4, 2), (4, 3);
INSERT INTO PizzaSizes (PizzaId, SizeId) VALUES (5, 1), (5, 2), (5, 3);
INSERT INTO PizzaSizes (PizzaId, SizeId) VALUES (6, 1), (6, 2), (6, 3);
INSERT INTO PizzaSizes (PizzaId, SizeId) VALUES (7, 1), (7, 2), (7, 3);
INSERT INTO PizzaSizes (PizzaId, SizeId) VALUES (8, 1), (8, 2), (8, 3);
INSERT INTO PizzaDoughTypes (PizzaId, DoughTypeId) VALUES (1, 1), (1, 2);
INSERT INTO PizzaDoughTypes (PizzaId, DoughTypeId) VALUES (2, 1), (2, 2);
INSERT INTO PizzaDoughTypes (PizzaId, DoughTypeId) VALUES (3, 1), (3, 2);
INSERT INTO PizzaDoughTypes (PizzaId, DoughTypeId) VALUES (4, 1), (4, 2);
INSERT INTO PizzaDoughTypes (PizzaId, DoughTypeId) VALUES (6, 1), (6, 2);
INSERT INTO PizzaDoughTypes (PizzaId, DoughTypeId) VALUES (7, 1), (7, 2);
INSERT INTO PizzaDoughTypes (PizzaId, DoughTypeId) VALUES (8, 1), (8, 2);