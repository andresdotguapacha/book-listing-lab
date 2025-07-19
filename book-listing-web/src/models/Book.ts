import { Author } from "./Author";

export interface Book {
  id: string;
  title: string;
  publicationDate: string;
  authors: Author[];
}
