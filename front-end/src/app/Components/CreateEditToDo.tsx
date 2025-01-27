"use client";
import React, { useState, useEffect } from "react";
import ToDoService from "@/Services/ToDoService";
import { IDoToRead } from "@/Types/IDoTORead";
import { IDoToWrite } from "@/Types/IDoToWrite";

interface CreateEditToDoModalProps {
  toDo?: IDoToRead;
  isOpen: boolean;
  onClose: () => void;
}

export default function CreateEditToDoModal({
  toDo,
  isOpen,
  onClose,
}: CreateEditToDoModalProps) {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [completed, setCompleted] = useState(false);
  const [dueDate, setDueDate] = useState<Date | undefined>();
  const [errors, setErrors] = useState<string[]>([]);

  useEffect(() => {
    if (toDo) {
      setTitle(toDo.title);
      setDescription(toDo.description);
      setCompleted(toDo.completed);
      setDueDate(new Date(toDo.dueDate));
    } else {
      resetForm();
    }
  }, [toDo, isOpen]);

  const resetForm = () => {
    setTitle("");
    setDescription("");
    setCompleted(false);
    setDueDate(undefined);
    setErrors([]);
  };

  const verifyInput = (): boolean => {
    const result: string[] = [];
    if (title.trim().length < 3) {
      result.push("Title must be longer than 3 characters");
    }
    if (description.trim().length < 3) {
      result.push("Description must be longer than 3 characters");
    }
    if (!dueDate) {
      result.push("Please select ToDo due date");
    }
    setErrors(result);
    return result.length === 0;
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!verifyInput()) return;

    const todoData: IDoToWrite = {
      title,
      description,
      completed,
      parentTaskId: null,
      dueDate: dueDate!,
      createdBy: "",
      updatedBy: "",
    };

    const service = new ToDoService<IDoToWrite>("");

    if (toDo) {
      const result = await service.edit(todoData, toDo.id);
      if (result.data) {
        onClose();
        window.location.reload();
      }
      if (result.errors) {
        setErrors(result.errors);
      }
    } else {
      const result = await service.create(todoData);
      if (result.data) {
        onClose();
        window.location.reload();
      }
      if (result.errors) {
        setErrors(result.errors);
      }
    }
  };

  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center overflow-x-hidden overflow-y-auto">
      <div className="relative w-full max-w-md p-4">
        <div className="relative bg-white rounded-lg shadow-xl p-6">
          <button
            onClick={onClose}
            className="absolute top-3 right-3 text-gray-400 hover:text-gray-600"
          >
            ✕
          </button>

          <h2 className="text-xl font-semibold mb-4">
            {toDo ? "Edit ToDo" : "Create New ToDo"}
          </h2>

          <form onSubmit={handleSubmit} className="space-y-4">
            {errors.length > 0 && (
              <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative">
                {errors.map((error, index) => (
                  <p key={index}>{error}</p>
                ))}
              </div>
            )}

            <div>
              <label className="block text-sm font-medium text-gray-700">
                Title
              </label>
              <input
                type="text"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                className="mt-1 block w-full border border-gray-300 rounded-md shadow-sm py-2 px-3"
                required
              />
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700">
                Description
              </label>
              <textarea
                value={description}
                onChange={(e) => setDescription(e.target.value)}
                className="mt-1 block w-full border border-gray-300 rounded-md shadow-sm py-2 px-3"
                required
              />
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700">
                Due Date
              </label>
              <input
                type="datetime-local"
                value={dueDate ? dueDate.toISOString().slice(0, 16) : ""}
                onChange={(e) => setDueDate(new Date(e.target.value))}
                className="mt-1 block w-full border border-gray-300 rounded-md shadow-sm py-2 px-3"
                required
              />
            </div>

            <div className="flex items-center">
              <input
                type="checkbox"
                checked={completed}
                onChange={(e) => setCompleted(e.target.checked)}
                className="h-4 w-4 text-indigo-600 focus:ring-indigo-500 border-gray-300 rounded"
              />
              <label className="ml-2 block text-sm text-gray-900">
                Completed
              </label>
            </div>

            <div className="flex justify-end space-x-2 mt-4">
              <button
                type="button"
                onClick={onClose}
                className="px-4 py-2 text-gray-600 bg-white border border-gray-300 rounded-lg hover:bg-gray-100"
              >
                Cancel
              </button>
              <button
                type="submit"
                className="px-4 py-2 text-white bg-indigo-600 rounded-lg hover:bg-indigo-700"
              >
                {toDo ? "Update" : "Create"}
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}
