import { useState } from "react";
import Card from "./Card";
import LoginForm from "./LoginForm"; // dodaj to jeśli brakuje
import DishForm from "./DishForm";

function App() {
  const [user, setUser] = useState(null);
  return (
      <div>
        {user ? (
            <DishForm />
        ) : (
            <LoginForm onLoginSuccess={(user) => setUser(user)} />
        )}
      </div>
  );
}

export default App;
