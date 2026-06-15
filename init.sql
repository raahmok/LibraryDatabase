CREATE TABLE IF NOT EXISTS Books (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    isbn TEXT NOT NULL,
    title TEXT NOT NULL,
    author TEXT NOT NULL,
    year INTEGER,
    genre TEXT NOT NULL,
    copies INTEGER,
    available INTEGER
);

CREATE TABLE IF NOT EXISTS Users (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    usr_id INTEGER NOT NULL,
    username TEXT NOT NULL,
    email TEXT NOT NULL,
    password TEXT NOT NULL,
    admin INTEGER,
    borrowed_books INTEGER,
    penalty INTEGER
);

CREATE TABLE IF NOT EXISTS Borrowed (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    book_id INTEGER,
    member_id INTEGER,
    borrow_date DATE,
    return_date DATE,
    return_deadline DATE,
    FOREIGN KEY (book_id) REFERENCES Books (id),
    FOREIGN KEY (member_id) REFERENCES Users (id)
)