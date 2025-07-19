import React, { useState, useMemo } from "react";
import { useBooks } from "./hooks/useBooks";
import { Book } from "./models/Book";
import { BookForm } from "./components/bookForm";
import "./global.css";

type SortKey = "title" | "publicationDate" | "authorCount";

const App: React.FC = () => {
  const { books, loading, error, addBook, updateBook, deleteBook } = useBooks();
  const [editingBook, setEditingBook] = useState<Book | null>(null);

  const [filterTitle, setFilterTitle] = useState("");
  const [filterStartDate, setFilterStartDate] = useState("");
  const [filterEndDate, setFilterEndDate] = useState("");
  const [filterMinAuthors, setFilterMinAuthors] = useState<number | "">("");
  const [filterMaxAuthors, setFilterMaxAuthors] = useState<number | "">("");

  const [sortKey, setSortKey] = useState<SortKey>("title");
  const [sortAsc, setSortAsc] = useState(true);

  const filteredAndSortedBooks = useMemo(() => {
    let filtered = books;

    if (filterTitle.trim() !== "") {
      filtered = filtered.filter((b) =>
        b.title.toLowerCase().includes(filterTitle.toLowerCase())
      );
    }

    if (filterStartDate) {
      filtered = filtered.filter((b) => b.publicationDate >= filterStartDate);
    }

    if (filterEndDate) {
      filtered = filtered.filter((b) => b.publicationDate <= filterEndDate);
    }

    if (filterMinAuthors !== "") {
      filtered = filtered.filter((b) => b.authors.length >= filterMinAuthors);
    }

    if (filterMaxAuthors !== "") {
      filtered = filtered.filter((b) => b.authors.length <= filterMaxAuthors);
    }

    const sorted = [...filtered].sort((a, b) => {
      let compare = 0;
      if (sortKey === "title") {
        compare = a.title.localeCompare(b.title);
      } else if (sortKey === "publicationDate") {
        compare = a.publicationDate.localeCompare(b.publicationDate);
      } else if (sortKey === "authorCount") {
        compare = a.authors.length - b.authors.length;
      }
      return sortAsc ? compare : -compare;
    });

    return sorted;
  }, [
    books,
    filterTitle,
    filterStartDate,
    filterEndDate,
    filterMinAuthors,
    filterMaxAuthors,
    sortKey,
    sortAsc,
  ]);

  const handleSave = async (book: Omit<Book, "id">, id?: string) => {
    if (id) {
      await updateBook(id, book);
    } else {
      await addBook(book);
    }
    setEditingBook(null);
  };

  return (
    <div id="app-root">
      <h1>Digital Library</h1>

      {error && <p className="error-text">{error}</p>}
      {loading && <p>Loading...</p>}

      <div className="three-column-layout">
        {/* Left column: Contextual menu (BookForm) */}
        <section className="column contextual-menu">
          <BookForm
            onSave={handleSave}
            initialData={editingBook ?? undefined}
            onCancel={() => setEditingBook(null)}
          />
        </section>

        {/* Middle column: Filters & Sorting */}
        <section className="column filters">
          <h2>Filters & Sorting</h2>
          <form className="filters-form" onSubmit={(e) => e.preventDefault()}>
            <div>
              <span className="field-label">Enter title</span>
              <input
                placeholder="Enter title"
                value={filterTitle}
                onChange={(e) => setFilterTitle(e.target.value)}
              />
            </div>

            <div>
              <span className="field-label">Start Date</span>
              <input
                type="date"
                value={filterStartDate}
                onChange={(e) => setFilterStartDate(e.target.value)}
              />
            </div>

            <div>
              <span className="field-label">End Date</span>
              <input
                type="date"
                value={filterEndDate}
                onChange={(e) => setFilterEndDate(e.target.value)}
              />
            </div>

            <div>
              <span className="field-label">Min Authors</span>
              <input
                type="number"
                min={0}
                value={filterMinAuthors}
                onChange={(e) =>
                  setFilterMinAuthors(
                    e.target.value === "" ? "" : Number(e.target.value)
                  )
                }
              />
            </div>

            <div>
              <span className="field-label">Max Authors</span>
              <input
                type="number"
                min={0}
                value={filterMaxAuthors}
                onChange={(e) =>
                  setFilterMaxAuthors(
                    e.target.value === "" ? "" : Number(e.target.value)
                  )
                }
              />
            </div>

            <div>
              <span className="field-label">Sort by</span>
              <select
                value={sortKey}
                onChange={(e) => setSortKey(e.target.value as SortKey)}
              >
                <option value="title">Title</option>
                <option value="publicationDate">Publication Date</option>
                <option value="authorCount">Number of Authors</option>
              </select>
            </div>

            <button
              type="button"
              onClick={() => setSortAsc(!sortAsc)}
              className="sort-button"
              aria-label="Toggle sort order"
            >
              Sort {sortAsc ? "Asc" : "Desc"}
            </button>
          </form>
        </section>

        {/* Right column: Books List */}
        <section className="column books-list-section">
          <h2>Books</h2>
          <ul className="books-list">
            {filteredAndSortedBooks.map((book) => (
              <li key={book.id} className="book-item">
                <strong>{book.title}</strong> ({book.publicationDate})
                <small>
                  Authors:{" "}
                  {book.authors
                    .map((a) => `${a.firstName} ${a.lastName}`)
                    .join(", ")}
                </small>
                <div className="book-actions">
                  <button
                    type="button"
                    onClick={() => setEditingBook(book)}
                    aria-label={`Edit book ${book.title}`}
                  >
                    Edit
                  </button>
                  <button
                    type="button"
                    onClick={() => deleteBook(book.id)}
                    className="delete-button"
                    aria-label={`Delete book ${book.title}`}
                  >
                    Delete
                  </button>
                </div>
              </li>
            ))}
            {filteredAndSortedBooks.length === 0 && <li>No books found.</li>}
          </ul>
        </section>
      </div>
    </div>
  );
};

export default App;
