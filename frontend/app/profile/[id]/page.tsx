"use client";

import { useParams } from "next/navigation";
import React from "react";

const Profile = async () => {
  const params = useParams();
  const response = await fetch(`http://localhost:5084/api/User/${params.id}`);
  const data = await response.json();
  return <div>Profile {data.name}</div>;
};

export default Profile;
