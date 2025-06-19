import {useState, useEffect} from "react";
import postIngredient from "./api/postIngredient";

const formInputs = [
    {
        label: "Nazwa",
        type: "text",
        className: "input input-primary",
        key: "name",
    },
    {
        label: "URL",
        type: "text",
        className: "input input-primary",
        key: "url",
    },
    {
        label: "Cena (zł)",
        type: "number",
        className: "input input-secondary",
        key: "price",
    },
];

const IngredientFormInputs = ({ formIngredients }) => {
    const [ingredient, setIngredient] = useState({
        name: "",
        url: "",
        price: "",
    });
    const [errors, setErrors] = useState({});

    const handleInputChange = (key, value) => {
        setIngredient((prev) => ({ ...prev, [key]: value }));
        setErrors((prev) => ({ ...prev, [key]: "" })); // Czyści błąd dla danego pola
    };

    const validateForm = () => {
        const newErrors = {};
        if (!dish.name.trim()) newErrors.name = "Nazwa nie może być pusta.";
        if (!dish.url.trim()) newErrors.url = "URL nie może być pusty.";
        if (!dish.price || isNaN(dish.price) || parseFloat(dish.price) <= 0) {
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
        const ingredientToSent = {
            Name: ingredient.name,
            Url: ingredient.url,
            Price: ingredient.price,
        };
        try {
            await postIngredient(ingredientToSent);
            alert("✅ Składnik został utworzony!");
            setIngredient({ name: "", url: "", price: "" });
        } catch {
            alert("❌ Błąd przy tworzeniu składników.");
        }
    };

    return (
        <div className="inputs pt-8 w-1/2 pl-12">
            {formInputs.map((input, index) => (
                <div className="flex flex-col mb-6" key={index}>
                    <label className="text-lg font-medium mb-2">{input.label}</label>
                        <input
                            type={input.type}
                            placeholder={input.label}
                            className={`${input.className} ${
                                errors[input.key] ? "border-red-500" : ""
                            }`}
                            value={ingredient[input.key]}
                            onChange={(e) => handleInputChange(input.key, e.target.value)}
                        />
                    {errors[input.key] && (
                        <span className="text-red-500 text-sm mt-1">
              {errors[input.key]}
            </span>
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