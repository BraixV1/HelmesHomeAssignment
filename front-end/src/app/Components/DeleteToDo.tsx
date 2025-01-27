import ToDoService from "@/Services/ToDoService";
import { IDoToRead } from "@/Types/IDoTORead";
import React from "react";

interface DeleteToDoProps {
  toDoId: string;
  onClose: () => void;
}

export default function DeleteToDO({ toDoId, onClose }: DeleteToDoProps) {
  const handleDelete = async () => {
    const result = await new ToDoService<IDoToRead>("").delete(toDoId);

    if (result.data) {
      window.location.reload();
    }

    onClose();
  };

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center overflow-x-hidden overflow-y-auto outline-none focus:outline-none">
      <div className="relative w-full max-w-md p-4">
        <div className="relative bg-white rounded-lg shadow-xl">
          <button
            onClick={onClose}
            className="absolute top-3 right-3 text-gray-400 hover:text-gray-600"
          >
            ✕
          </button>

          <div className="p-6 text-center">
            <svg
              className="w-12 h-12 mx-auto mb-4 text-gray-400"
              fill="none"
              stroke="currentColor"
              viewBox="0 0 24 24"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth="2"
                d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z"
              />
            </svg>

            <h3 className="mb-5 text-lg font-normal text-gray-500">
              Are you sure you want to delete this ToDo?
            </h3>

            <div className="flex justify-center space-x-4">
              <button
                onClick={handleDelete}
                className="px-4 py-2 text-white bg-red-600 rounded-lg hover:bg-red-700"
              >
               {"Yes, I'm sure"}
              </button>
              <button
                onClick={onClose}
                className="px-4 py-2 text-gray-600 bg-white border border-gray-300 rounded-lg hover:bg-gray-100"
              >
                No, cancel
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
