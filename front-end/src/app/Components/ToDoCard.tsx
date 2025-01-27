import React, { useState } from "react";
import { IDoToRead } from "@/Types/IDoTORead";
import DeleteToDo from "./DeleteToDo";
import CreateEditToDoModal from "./CreateEditToDo";
import ToDoService from "@/Services/ToDoService";

interface ToDoCardProps {
  toDo: IDoToRead;
}

export default function ToDoCard({ toDo }: ToDoCardProps) {
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);

  const toggleDeleteModal = () => setIsDeleteModalOpen(!isDeleteModalOpen);
  const toggleEditModal = () => setIsEditModalOpen(!isEditModalOpen);

  const handleComplete = async () => {
    toDo.completed = true;
    const result = await new ToDoService<IDoToRead>("").edit(toDo, toDo.id);

    if (result.data) {
      window.location.reload();
    }
  };

  return (
    <div className="bg-white border-2 border-gray-200 rounded-lg shadow-md p-4 space-y-3">
      <div className="flex justify-between items-center">
        <h2 className="text-lg font-bold text-gray-800">{toDo.title}</h2>
        <span className="text-sm text-gray-600">
          Due: {new Date(toDo.dueDate).toLocaleString()}
        </span>
      </div>
      <div className="flex justify-between items-start">
        <div className="space-y-2">
          <p className="text-gray-700">{toDo.description}</p>
          <p className="text-sm text-gray-500">
            Created: {new Date(toDo.createdAt).toLocaleDateString()}
          </p>
        </div>
        <div className="flex space-x-2">
          {!toDo.completed ? (
            <button
              className="px-3 py-1 text-sm text-gray-700 border border-gray-300 rounded hover:bg-gray-100"
              onClick={handleComplete}
            >
              Complete
            </button>
          ) : (
            <div></div>
          )}

          <button
            className="px-3 py-1 text-sm text-gray-700 border border-gray-300 rounded hover:bg-gray-100"
            onClick={toggleEditModal}
          >
            Edit
          </button>
          <button
            className="px-3 py-1 text-sm text-red-600 border border-red-300 rounded hover:bg-red-50"
            onClick={toggleDeleteModal}
          >
            Delete
          </button>
        </div>
      </div>
      {isDeleteModalOpen && (
        <DeleteToDo toDoId={toDo.id} onClose={toggleDeleteModal} />
      )}
      <CreateEditToDoModal
        toDo={toDo}
        isOpen={isEditModalOpen}
        onClose={() => setIsEditModalOpen(false)}
      />
    </div>
  );
}
