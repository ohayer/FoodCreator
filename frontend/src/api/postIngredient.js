export default async function postIngredient(ingredientData) {
    const response = await fetch("https://localhost:44385/api/Ingredients", {
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
