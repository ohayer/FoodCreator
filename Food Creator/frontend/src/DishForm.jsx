import UrlInput from "./UrlInput";
const formInputs = [
  {
    label: "Nazwa",
    type: "text",
    className: "input input-primary",
  },
  {
    label: "Zdjęcie (url)",
    type: "url",
    className: "url-input",
  },
  {
    label: "Cena w zł",
    type: "number",
    className: "input input-secondary",
  },
];

const DishForm = ({ dish, onSubmit }) => {
  return (
    <div className="p-16">
      <h4 className="text-6xl mb-14">Stwórz własne danie:</h4>
      <div className="flex">
        <div className="inputs pt-8 w-1/2">
          {formInputs.map((input, index) => (
            <div className="flex flex-col mb-6">
              <label className="text-lg font-medium mb-2">{input.label}</label>
              {input.type === "url" ? (
                <UrlInput />
              ) : (
                <input
                  type={input.type}
                  placeholder={input.label}
                  className={input.className}
                />
              )}
            </div>
          ))}
          <button className="btn btn-active btn-accent text-xl mt-6">
            Stwórz danie
          </button>
        </div>
        <div className="w-1/2">
          <h4 className="pl-4 text-2xl">Składniki</h4>
        </div>
      </div>
    </div>
  );
};
export default DishForm;
