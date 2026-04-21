import React from "react";
function GegevensBox({ titel, gegevens, formData = {}, onChange }) {

    return (

        <div style={{
            width: "100%", height: "auto", border: "1px solid ", margin: "20px 0px", padding: "20px", boxSizing: "border-box"
        }}>

            <div style={{ margin: "20px 0px" }}>
                <h3 className="titel" style={{ margin: "0px" }}>{titel}</h3>
                <div className="lijntje" style={{ width: "auto" }}></div>
            </div>

            {gegevens.map((veld, index) => {
                const inputId = veld.naam.toLowerCase().replace(/\s+/g, '-');

                return (
                    <div key={index} style={{ display: "flex", flexDirection: "column", flex: 1, marginBottom: "20px" }}>

                        <label htmlFor={inputId} style={{ fontWeight: "bold", fontSize: "14px", marginBottom: "5px" }}>
                            {veld.naam}
                        </label>

                        <input
                            type={veld.type || "text"}
                            id={inputId}
                            name={inputId}
                            value={formData[inputId] ?? ""}
                            onChange={(e) => onChange?.(inputId, e.target.value)}
                            style={{ padding: "10px", border: "1px solid #ccc", borderRadius: "4px", fontSize: "14px" }}
                            autoComplete={veld.type === "password" ? "new-password" : undefined}
                        />

                    </div>
                );
            })}


        </div>

    );
}
export default GegevensBox;