import { useState } from "react";
import { signInWithEmailAndPassword } from "firebase/auth";
import { auth } from "./firebase";

const LoginForm = ({ onLoginSuccess }) => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const login = async () => {
        try {
            const res = await signInWithEmailAndPassword(auth, email, password);
            alert("Zalogowano!");
            onLoginSuccess(res.user);
        } catch (err) {
            alert("Błąd logowania: " + err.message);
        }
    };

    return (
        <div className="max-w-sm mx-auto mt-12 p-6 border rounded-lg shadow">
            <h2 className="text-xl font-bold mb-4">Logowanie</h2>
            <input
                type="email"
                placeholder="Email"
                className="input input-bordered mb-3 w-full"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
            />
            <input
                type="password"
                placeholder="Hasło"
                className="input input-bordered mb-4 w-full"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
            />
            <button onClick={login} className="btn btn-success w-full">
                Zaloguj się
            </button>
        </div>
    );
};

export default LoginForm;
