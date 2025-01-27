export interface IDoToWrite {
  title: string;
  description: string;
  completed: boolean;
  parentTaskId: string | null;
  dueDate: Date;
  updatedBy: string;
  createdBy: string;
}
