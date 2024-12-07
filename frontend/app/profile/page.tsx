"use client";
import React, { useEffect, useState } from "react";
import LoadingSpinner from "../Components/LoadingSpinner";
import Link from "next/link";

interface User {
  id: number;
  name: string;
  email: string;
  age: number;
}

const Users = () => {
  const [data, setData] = useState<User[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      const response = await fetch("http://localhost:5084/api/User");
      const result = await response.json();
      setData(result);
      setLoading(false);
    };

    fetchData();
  }, []);

  if (loading) return <LoadingSpinner />;

  return (
    <div className="flex flex-col items-center justify-center h-screen">
      <h1 className="text-2xl font-bold">Users</h1>
      <div>
        {data.map((user: User) => (
          <Link href={`/profile/${user.id}`}>
            <div
              key={user.id}
            className="flex flex-col border-2 border-gray-300 rounded-md p-2 mt-4"
          >
            <span> Id : {user.id}</span>
            <span> Name : {user.name}</span>
            <span> Email : {user.email}</span>
            <span> Age : {user.age}</span>
          </div>
          </Link>
        ))}
      </div>
    </div>
  );
};

export default Users;
