"use client";
import { useRouter, useSearchParams } from "next/navigation";
import { useState } from "react";

export function FilterBar() {
  const router = useRouter();
  const searchParams = useSearchParams();

  const [filters, setFilters] = useState({
    done: searchParams.get("done") || "",
    description: searchParams.get("description") || "",
    dueDateTime: searchParams.get("dueDateTime") || "",
    pageSize: searchParams.get("pageSize") || "10",
  });

  const applyFilters = () => {
    const params = new URLSearchParams();

    Object.entries(filters).forEach(([key, value]) => {
      if (value) {
        const formattedValue =
          key === "dueDateTime" ? new Date(value).toISOString() : value;
        params.append(key, formattedValue.toString());
      }
    });

    router.push(`?${params.toString()}`);
    router.refresh();
  };

  const resetFilter = () => {
    setFilters({
      done: "",
      description: "",
      dueDateTime: "",
      pageSize: "10",
    });
    applyFilters();
  };

  return (
    <div className="flex space-x-2">
      <div className="flex flex-row space-x-1 h-min mt-6">
        <select
          value={filters.done}
          onChange={(e) =>
            setFilters((prev) => ({ ...prev, done: e.target.value }))
          }
        >
          <option value="">All</option>
          <option value="true">Done</option>
          <option value="false">unDone</option>
        </select>

        <input
          type="text"
          placeholder="Filter by content"
          value={filters.description}
          onChange={(e) =>
            setFilters((prev) => ({ ...prev, description: e.target.value }))
          }
        />
      </div>

      <div>
        <label className="block text-sm font-medium text-gray-700">
          Due Date
        </label>
        <input
          type="datetime-local"
          value={filters.dueDateTime}
          onChange={(e) =>
            setFilters((prev) => ({ ...prev, dueDateTime: e.target.value }))
          }
          className="mt-1 block w-full border border-gray-300 rounded-md shadow-sm py-2 px-3"
          required
        />
      </div>

      <div>
        <label className="block text-sm font-medium text-gray-700">
          Page Size
        </label>
        <select
          value={filters.pageSize}
          onChange={(e) =>
            setFilters((prev) => ({ ...prev, pageSize: e.target.value }))
          }
          className="mt-1 block w-full border border-gray-300 rounded-md shadow-sm py-2 px-3"
        >
          <option value="5">5</option>
          <option value="10">10</option>
          <option value="20">20</option>
          <option value="50">50</option>
        </select>
      </div>

      <div className="flex flex-col gap-4">
        <button className="bg-blue-600 rounded-md p-2" onClick={applyFilters}>
          Apply Filters
        </button>
        <button className="bg-red-600 rounded-md p-2" onClick={resetFilter}>
          Reset filters
        </button>
      </div>
    </div>
  );
}
