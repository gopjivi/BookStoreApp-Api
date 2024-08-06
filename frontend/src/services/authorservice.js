export function checkAuthorName(name) {
  return fetch(`https://localhost:7088/api/Authors/CheckAuthorNameIsExists/${name}`, {
    method: "GET",
    credentials: "same-origin",
    headers: {
      "Content-Type": "application/json",
    },
  })
    .then((response) => {
      if (!response.ok) {
        return response.json().then((error) => {
          throw new Error(
            error.message || "Failed to check authorname availability"
          );
        });
      }
      return response.json();
    })
    .catch((error) => {
      console.error("Error checking authorname availability:", error);
      throw error;
    });
}

export function checkAuthorNameForEdit(id, name) {
  return fetch(
    `https://localhost:7088/api/Authors/CheckAuthorNameIsExistsForUpdate/${id}/${name}`,
    {
      method: "GET",
      credentials: "same-origin",
      headers: {
        "Content-Type": "application/json",
      },
    }
  )
    .then((response) => {
      if (!response.ok) {
        return response.json().then((error) => {
          throw new Error(
            error.message || "Failed to check authorname availability"
          );
        });
      }
      return response.json();
    })
    .catch((error) => {
      console.error("Error checking authorname availability:", error);
      throw error;
    });
}

export function checkAuthorExistsInBook(id) {
  return fetch(`https://localhost:7088/api/Authors/CheckAuthorExistsInBook/${id}`, {
    method: "GET",
    credentials: "same-origin",
    headers: {
      "Content-Type": "application/json",
    },
  })
    .then((response) => {
      if (!response.ok) {
        return response.json().then((error) => {
          throw new Error(
            error.message || "Failed to check authorname availability"
          );
        });
      }
      return response.json();
    })
    .catch((error) => {
      console.error("Error checking authorname availability:", error);
      throw error;
    });
}
