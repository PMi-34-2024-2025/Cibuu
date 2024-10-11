-- Створення таблиці для користувачів (Users)
CREATE TABLE Users (
    user_id SERIAL PRIMARY KEY,  -- Унікальний ідентифікатор користувача
    username VARCHAR(50) NOT NULL,  -- Ім'я користувача
    favorite_places TEXT[],  -- Масив улюблених місць (може містити кілька закладів)
    email VARCHAR(100) NOT NULL  -- Електронна пошта
);

-- Створення таблиці для закладів (Restaurants)
CREATE TABLE Restaurants (
    restaurant_id SERIAL PRIMARY KEY,  -- Унікальний ідентифікатор закладу
    name VARCHAR(100) NOT NULL,  -- Назва закладу
    description TEXT,  -- Опис закладу
    reviews TEXT[],  -- Масив відгуків
    location VARCHAR(255)  -- Локація закладу
);

-- Створення таблиці для відгуків (Reviews)
CREATE TABLE Reviews (
    review_id SERIAL PRIMARY KEY,  -- Унікальний ідентифікатор відгуку
    user_id INT REFERENCES Users(user_id) ON DELETE CASCADE,  -- Відгук пов'язаний із користувачем
    restaurant_id INT REFERENCES Restaurants(restaurant_id) ON DELETE CASCADE,  -- Відгук пов'язаний із закладом
    rate INT CHECK (rate >= 1 AND rate <= 5),  -- Оцінка (від 1 до 5)
    text TEXT,  -- Текст відгуку
    review_date DATE DEFAULT CURRENT_DATE  -- Дата створення відгуку
);

-- Створення таблиці для адміністраторів (Admins)
CREATE TABLE Admins (
    admin_id SERIAL PRIMARY KEY,  -- Унікальний ідентифікатор адміністратора
    role VARCHAR(50),  -- Роль адміністратора
    username VARCHAR(50) NOT NULL  -- Ім'я користувача адміністратора
);