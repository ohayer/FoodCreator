import { useState, useEffect } from "react";
import { onAuthStateChanged, signOut } from "firebase/auth";
import { auth } from "./firebase";
import DishForm from "./DishForm";
import LoginForm from "./LoginForm";

function App() {
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true); // aby poczekać na sprawdzenie sesji

    useEffect(() => {
        const unsubscribe = onAuthStateChanged(auth, (firebaseUser) => {
            setUser(firebaseUser);
            setLoading(false);
        });

        return () => unsubscribe(); // wyczyść nasłuchiwanie
    }, []);

    const handleLogout = async () => {
        await signOut(auth);
        setUser(null);
    };

    if (loading) return <p className="p-4">Ładowanie...</p>;

    return (
        <div>
            {user ? (
                <>
                    <button
                        className="btn btn-sm btn-error absolute top-4 right-4"
                        onClick={handleLogout}
                    >
                        Wyloguj
                    </button>
                    <DishForm />
                </>
            ) : (
                <LoginForm onLoginSuccess={setUser} />
            )}
        </div>
    );
}

export default App;
