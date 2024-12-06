import Image from "next/image";

export default async function Home() {
  const response = await fetch("http://localhost:5084/api/User");
  const data = await response.json();
  return <h1>Hello Swag {data.message}</h1>;
}
