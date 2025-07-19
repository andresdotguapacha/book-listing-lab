import axios from 'axios';
import { Book } from '../models/Book';

const API_URL = process.env.REACT_APP_API_URL as string;

if (!API_URL) {
  throw new Error('Missing REACT_APP_API_URL environment variable');
}

export const getAllBooks = async (): Promise<Book[]> => {
  const response = await axios.get<Book[]>(API_URL);
  return response.data;
};

export const getBookById = async (id: string): Promise<Book> => {
  const response = await axios.get<Book>(`${API_URL}/${id}`);
  return response.data;
};

export const addBook = async (book: Omit<Book, 'id'>): Promise<Book> => {
  const response = await axios.post<Book>(API_URL, book);
  return response.data;
};

export const updateBook = async (id: string, book: Omit<Book, 'id'>): Promise<void> => {
  await axios.put(`${API_URL}/${id}`, book);
};

export const deleteBook = async (id: string): Promise<void> => {
  await axios.delete(`${API_URL}/${id}`);
};
