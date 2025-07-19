import { useEffect, useState } from 'react';
import { Book } from '../models/Book';
import {
  getAllBooks,
  addBook,
  updateBook,
  deleteBook,
} from '../api/bookListingService';

export function useBooks() {
  const [books, setBooks] = useState<Book[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const loadBooks = async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await getAllBooks();
      setBooks(data);
    } catch {
      setError('Failed to load books.');
    } finally {
      setLoading(false);
    }
  };

  const handleAddBook = async (book: Omit<Book, 'id'>) => {
    setLoading(true);
    try {
      await addBook(book);
      await loadBooks();
    } catch {
      setError('Failed to add book.');
    } finally {
      setLoading(false);
    }
  };

  const handleUpdateBook = async (id: string, book: Omit<Book, 'id'>) => {
    setLoading(true);
    try {
      await updateBook(id, book);
      await loadBooks();
    } catch {
      setError('Failed to update book.');
    } finally {
      setLoading(false);
    }
  };

  const handleDeleteBook = async (id: string) => {
    setLoading(true);
    try {
      await deleteBook(id);
      await loadBooks();
    } catch {
      setError('Failed to delete book.');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadBooks();
  }, []);

  return {
    books,
    loading,
    error,
    addBook: handleAddBook,
    updateBook: handleUpdateBook,
    deleteBook: handleDeleteBook,
    reload: loadBooks,
  };
}
