"use client";
import Image from "next/image";
import { useState } from "react";

export default function Home() {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const handleSubmit = async (e: any) => {
    e.preventDefault();
    
  };

  return (
    <div className="flex flex-col items-center justify-center h-screen">
      <form
        action=""
        className="flex flex-col gap-4 border border-1 p-4 rounded-md"
        onSubmit={handleSubmit}
      >
        <input
          type="text"
          name="name"
          onChange={(e) => setName(e.target.value)}
        />
        <input
          type="text"
          name="email"
          onChange={(e) => setEmail(e.target.value)}
        />
        <input
          type="text"
          name="password"
          onChange={(e) => setPassword(e.target.value)}
        />
        <input type="submit" value="Submit" />
      </form>
    </div>
  );
}
