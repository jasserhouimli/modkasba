"use client";

import LoadingSpinner from "@/app/Components/LoadingSpinner";
import { useParams } from "next/navigation";
import React, { useEffect, useState } from "react";

interface User {
  id: number;
  name: string;
  email: string;
  age: number;
  title: string;
}

const Profile = () => {
  const [loading, setLoading] = useState(true);
  const [data, setData] = useState<User | null>(null);
  const params = useParams();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(
          `http://localhost:5084/api/User/${params.id}`
        );
        const res = await response.json();
        setData(res);
      } catch (error) {
        console.error("Error fetching user:", error);
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  }, [params.id]);

  if (loading) return <LoadingSpinner />;
  if (data?.title) return <div>User not found</div>;

  return (
    <div className="flex flex-col items-center justify-center h-screen">
      <div className="flex flex-col border-2 border-gray-300 rounded-md p-4">
        <h1 className="text-2xl font-bold mb-4">Profile</h1>
        {data ? (
          <>
            <span>Id: {data.id}</span>
            <span>Name: {data.name}</span>
            <span>Email: {data.email}</span>
            <span>Age: {data.age}</span>
          </>
        ) : (
          <span>User not found</span>
        )}
      </div>
    </div>
  );
};

export default Profile;
