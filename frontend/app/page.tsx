import LoadingSpinner from "./Components/LoadingSpinner";
import { Suspense } from "react";
import Profile from "./profile/page";

export default function Home() {
  return (
    <Suspense fallback={<LoadingSpinner />}>
      <div className="flex flex-col items-center justify-center h-screen font-bold text-4xl">
        Home
      </div>
    </Suspense>
  );
}
