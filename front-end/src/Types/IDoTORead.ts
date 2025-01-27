export interface IDoToRead {
  id: string;
  title: string;
  description: string;
  completed: boolean;
  dueDate: Date;
  parentTask: string | null;
  createdBy: string;
  createdAt: Date;
  updatedBy: string;
  updatedAt: Date;
}
