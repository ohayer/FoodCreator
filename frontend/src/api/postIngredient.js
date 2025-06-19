export default async function postIngredient(formData) {
    const response = await fetch("http://localhost:3000/api/Ingredients", {
        method: "POST",
        body: formData,
    });

    if (!response.ok) {
        throw new Error("Błąd podczas dodawania składników");
    }

    return await response.json();
}