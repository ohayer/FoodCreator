import { useState } from "react";
import Card from "./Card";
import DishForm from "./DishForm";

function App() {
  return (
    <div className="flex h-screen">
      <div className="w-1/2 flex items-start justify-start">
        <DishForm />
      </div>
      <div className="w-1/2 h-full">
        <Card />
      </div>
    </div>
  );
}

export default App;
