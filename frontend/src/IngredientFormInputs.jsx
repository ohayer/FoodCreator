import {useState, useEffect} from "react";
import postIngredient from "./api/postIngredient";
import "./index.css"

const formInputs = [
    {
        label: "Nazwa",
        type: "text",
        className: "input input-primary",
        key: "name",
    },
    {
        label: "Cena (zł)",
        type: "number",
        className: "input input-secondary",
        key: "price",
    },
    {
        label: "Zdjęcie",
        type: "file",
        accept: "image/*",
        className: "input input-primary",
        key: "image",
    }
];

const IngredientFormInputs = ({ formIngredients }) => {
    const [ingredient, setIngredient] = useState({
        name: "",
        price: "",
        image: null,
    });
    const [errors, setErrors] = useState({});

    const handleInputChange = (key, value) => {
        setIngredient((prev) => ({ ...prev, [key]: value }));
        setErrors((prev) => ({ ...prev, [key]: "" })); // Czyści błąd dla danego pola
    };

    const validateForm = () => {
        const newErrors = {};
        if (!ingredient.name.trim()) newErrors.name = "Nazwa nie może być pusta.";
        if (!ingredient.price || isNaN(ingredient.price) || parseFloat(ingredient.price) <= 0) {
            newErrors.price = "Cena musi być liczbą większą od zera.";
        }
        setErrors(newErrors);
        if (Object.keys(newErrors).length > 0) {
            alert("Uzupełnij wszystkie pola poprawnie.");
            return false;
        }
        return true;
    };

    const handleSubmit = async () => {
        // if (!validateForm()) return; // Jeśli są błędy, zatrzymaj wysyłanie
        const formData = new FormData();
        console.log("Ingredient before sending:", ingredient);
        formData.append("Name", ingredient.name);
        formData.append("Price", ingredient.price);
        if (ingredient.image) {
            formData.append("Image", ingredient.image);
        }
        try {
            await postIngredient(formData);
            alert("✅ Składnik został utworzony!");
            setIngredient({ name: "", price: "" , image: null});
        } catch {
            alert("❌ Błąd przy tworzeniu dania.");
        }
    };

    return (
        <div className="inputs pt-8 pl-12">
            {formInputs.map((input, index) => (
                <div className="flex flex-col mb-6" key={index} >
                    <label className="text-lg font-medium mb-2">{input.label}</label>
                    <input
                        type={input.type}
                        accept={input.accept}
                        placeholder={input.label}
                        className={`${input.className} ${errors[input.key] ? "border-red-500" : ""}`}
                        value={input.type === "file" ? undefined : ingredient[input.key]}
                        onChange={(e) => {
                            const value=
                                input.type === "file" ? e.target.files[0] : e.target.value;
                            handleInputChange(input.key, value);
                        }}
                    />
                    {errors[input.key] && (
                        <span className="text-red-500 text-sm mt-1"></span>
                    )}
                </div>
            ))}
            <button
                className="btn btn-success mt-2 mb-8"
                onClick={handleSubmit}
            >
                Stwórz składnik
            </button>
        </div>
    );
};

export default IngredientFormInputs;