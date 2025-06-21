import { useState } from "react";
import DishForm from "./DishForm";
import LoginForm from "./LoginForm";

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
