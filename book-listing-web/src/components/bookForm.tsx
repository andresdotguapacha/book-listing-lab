import React, { useState } from "react";
import { Book } from "../models/Book";
import { v4 as uuidv4 } from "uuid";

type Props = {
  onSave: (book: Omit<Book, "id">, id?: string) => void;
  initialData?: Book;
  onCancel?: () => void;
};

export const BookForm: React.FC<Props> = ({
  onSave,
  initialData,
  onCancel,
}) => {
  const [title, setTitle] = useState(initialData?.title ?? "");
  const [publicationDate, setPublicationDate] = useState(
    initialData?.publicationDate ?? ""
  );
  const [authors, setAuthors] = useState(
    initialData?.authors ?? [{ id: uuidv4(), firstName: "", lastName: "" }]
  );

  const handleAuthorChange = (
    index: number,
    field: "firstName" | "lastName",
    value: string
  ) => {
    const updated = [...authors];
    updated[index][field] = value;
    setAuthors(updated);
  };

  const addAuthor = (e: React.MouseEvent) => {
    e.preventDefault();
    setAuthors([...authors, { id: uuidv4(), firstName: "", lastName: "" }]);
  };

  const removeAuthor = (index: number, e: React.MouseEvent) => {
    e.preventDefault();
    if (authors.length === 1) return;
    setAuthors(authors.filter((_, i) => i !== index));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    if (
      !title.trim() ||
      !publicationDate ||
      authors.some((a) => !a.firstName.trim() || !a.lastName.trim())
    ) {
      alert("All fields are required");
      return;
    }

    const book: Omit<Book, "id"> = {
      title,
      publicationDate,
      authors,
    };

    onSave(book, initialData?.id);
  };

  return (
    <form onSubmit={handleSubmit} style={{ marginBottom: 20 }}>
      <h3>{initialData ? "Edit Book" : "Add Book"}</h3>

      <div>
        <span className="field-label">Title</span>
        <input
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          placeholder="Enter book title"
        />
      </div>

      <div>
        <span className="field-label">Publication Date</span>
        <input
          type="date"
          value={publicationDate}
          onChange={(e) => setPublicationDate(e.target.value)}
        />
      </div>

      <h4>Authors</h4>
      {authors.map((author, index) => (
        <div key={author.id} style={{ marginBottom: 16 }}>
          <span className="field-label">First Name</span>
          <input
            placeholder="Type Here..."
            value={author.firstName}
            onChange={(e) =>
              handleAuthorChange(index, "firstName", e.target.value)
            }
          />

          <span className="field-label">Last Name</span>
          <input
            placeholder="Type Here..."
            value={author.lastName}
            onChange={(e) =>
              handleAuthorChange(index, "lastName", e.target.value)
            }
          />

          <button
            type="button"
            onClick={(e) => removeAuthor(index, e)}
            className="remove-author"
          >
            Remove
          </button>
        </div>
      ))}

      <button type="button" onClick={addAuthor}>
        Add Author
      </button>

      <br />

      <button type="submit" style={{ marginRight: 8 }}>
        Save
      </button>
      {onCancel && (
        <button type="button" onClick={onCancel}>
          Cancel
        </button>
      )}
    </form>
  );
};
