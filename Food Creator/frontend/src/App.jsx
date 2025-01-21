import { useState } from "react";
import "./App.css";

function App() {
  return (
    <div className="flex flex-col items-center justify-center h-screen">
      <div className="bg-white p-4 rounded-lg shadow-lg">
        <input
          type="text"
          placeholder="Nazwa Dania"
          className="input input-primary"
        />
      </div>
      <div></div>
    </div>
  );
}

export default App;
