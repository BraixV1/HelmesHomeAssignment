"use client";
import ToDoService from "@/Services/ToDoService";
import { IDoToRead } from "@/Types/IDoTORead";
import { IPaginationObject } from "@/Types/IPaginationObject";
import { useEffect, useState, Suspense } from "react";
import ToDoCard from "./Components/ToDoCard";
import CreateEditToDoModal from "./Components/CreateEditToDo";
import { useSearchParams } from "next/navigation";
import { FilterBar } from "./Components/FilterBar";

function ToDoList() {
  const searchParams = useSearchParams();
  const [toDoPagination, setToDoPagination] = useState<
    IPaginationObject<IDoToRead>
  >({
    items: [],
    pageNumber: 1,
    totalCount: 0,
    pageSize: 10,
  });
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);

  const toggleEditModal = () => setIsEditModalOpen(!isEditModalOpen);

  const fetchToDos = async () => {
    const service = new ToDoService<IDoToRead>(`/?${searchParams.toString()}`);
    const result = await service.getAll();

    if (result.data) {
      setToDoPagination(result.data);
    }
  };

  useEffect(() => {
    fetchToDos();
  }, [searchParams]);

  const handlePageChange = (page: number) => {
    const params = new URLSearchParams(searchParams.toString());
    params.set("page", page.toString());
    window.location.search = params.toString();
  };

  return (
    <div className="container mx-auto px-4 py-6 space-y-4">
      <button onClick={() => toggleEditModal()}>Add new toDo</button>
      <FilterBar />
      {toDoPagination?.items.map((toDo) => (
        <ToDoCard key={toDo.id} toDo={toDo} />
      ))}

      <div className="flex justify-center space-x-2 mt-4">
        <button
          onClick={() => handlePageChange(toDoPagination!.pageNumber - 1)}
          disabled={toDoPagination?.pageNumber === 1}
          className="px-4 py-2 bg-blue-500 text-white rounded disabled:bg-gray-300"
        >
          Previous
        </button>
        <span>
          Page {toDoPagination?.pageNumber} of{" "}
          {Math.floor(toDoPagination?.totalCount / toDoPagination?.pageSize) +
            1}
        </span>
        <button
          onClick={() => handlePageChange(toDoPagination?.pageNumber + 1)}
          disabled={
            toDoPagination?.pageNumber ===
            Math.floor(
              toDoPagination?.totalCount / toDoPagination!.pageSize + 1,
            )
          }
          className="px-4 py-2 bg-blue-500 text-white rounded disabled:bg-gray-300"
        >
          Next
        </button>
      </div>

      <CreateEditToDoModal
        isOpen={isEditModalOpen}
        onClose={() => setIsEditModalOpen(false)}
      />
    </div>
  );
}

export default function Home() {
  return (
    <Suspense fallback={<div>Loading...</div>}>
      <ToDoList />
    </Suspense>
  );
}
