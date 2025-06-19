export default async function postIngredient(ingredientData) {
    const response = await fetch("http://localhost:3000/api/Ingredients", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(ingredientData)
    });

    if (!response.ok) {
        throw new Error("Błąd podczas dodawania składników");
    }

    return response.json();
}
