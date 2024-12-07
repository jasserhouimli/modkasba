import React from "react";

interface User {
  id: number;
  name: string;
  email: string;
  age: number;
}

const Users = async () => {
  const response = await fetch("http://localhost:5084/api/User");
  const data = await response.json();
  return (
    <div className="flex flex-col items-center justify-center h-screen">
      <h1 className="text-2xl font-bold">Users</h1>
      <div>
        {data.map((user: User) => (
          <div
            key={user.id}
            className="flex flex-col border-2 border-gray-300 rounded-md p-2 mt-4"
          >
            <span> Id : {user.id}</span>
            <span> Name : {user.name}</span>
            <span> Email : {user.email}</span>
            <span> Age : {user.age}</span>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Users;
