import { useState } from "react";
import axios from "axios";
import UrlInput from "./UrlInput";

const formInputs = [
  {
    label: "Nazwa",
    type: "text",
    className: "input input-primary",
    key: "name",
  },
  {
    label: "Zdjęcie (url)",
    type: "url",
    className: "url-input",
    key: "url",
  },
  {
    label: "Cena w zł",
    type: "number",
    className: "input input-secondary",
    key: "price",
  },
];

const DishFormInputs = ({ formIngredients }) => {
  const [dish, setDish] = useState({
    name: "",
    url: "",
    price: "",
  });
  const [errors, setErrors] = useState({});

  const handleInputChange = (key, value) => {
    setDish((prev) => ({ ...prev, [key]: value }));
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
    return Object.keys(newErrors).length === 0; // Zwraca true, jeśli brak błędów
  };

  const handleSubmit = async () => {
    // if (!validateForm()) return; // Jeśli są błędy, zatrzymaj wysyłanie
    console.log(formIngredients);
    // try {
    //   const response = await axios.put("http://localhost:8080", dish, {
    //     headers: {
    //       "Content-Type": "application/json",
    //     },
    //   });
    //   console.log("Danie zostało zapisane:", response.data);
    //   alert("Danie zostało pomyślnie zapisane!");
    // } catch (error) {
    //   console.error("Błąd podczas zapisywania dania:", error);
    //   alert("Wystąpił problem podczas zapisywania dania.");
    // }
  };

  return (
    <div className="inputs pt-8 w-1/2 pl-12">
      {formInputs.map((input, index) => (
        <div className="flex flex-col mb-6" key={index}>
          <label className="text-lg font-medium mb-2">{input.label}</label>
          {input.type === "url" ? (
            <UrlInput
              value={dish.url}
              onChange={(e) => handleInputChange("url", e.target.value)}
            />
          ) : (
            <input
              type={input.type}
              placeholder={input.label}
              className={`${input.className} ${
                errors[input.key] ? "border-red-500" : ""
              }`}
              value={dish[input.key]}
              onChange={(e) => handleInputChange(input.key, e.target.value)}
            />
          )}
          {errors[input.key] && (
            <span className="text-red-500 text-sm mt-1">
              {errors[input.key]}
            </span>
          )}
        </div>
      ))}
      <button
        className="btn btn-active btn-accent text-xl mt-6"
        onClick={handleSubmit}
      >
        Stwórz danie
      </button>
    </div>
  );
};

export default DishFormInputs;
