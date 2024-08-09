import { getAPIToken } from './config';

export function checkGenreName(name) {
  return fetch(`https://localhost:7088/api/Genres/CheckGenreNameIsExists/${name}`, {
    method: "GET",
    credentials: "same-origin",
    headers: {
      "Content-Type": "application/json",
      "Authorization": `Bearer ${getAPIToken()}`, // Get the token from config
    },
  })
    .then((response) => {
      if (!response.ok) {
        // Check the content type of the response
        const contentType = response.headers.get("Content-Type");
        if (contentType && contentType.includes("application/json")) {
          // If the content type is JSON, parse it and throw an error with the message
          return response.json().then((error) => {
            throw new Error(
              error.message || "Failed to check genre name availability"
            );
          });
        } else {
          // If the content type is not JSON, throw a generic error
          throw new Error("Failed to check genre name availability");
        }
      }
      // If the response is ok, return the JSON data (which is expected to be a boolean)
      return response.json();
    })
    .then((data) => {
      // Assuming the API returns a boolean value directly
      return data;
    })
    .catch((error) => {
      console.error("Error checking genre name availability:", error);
      throw error;
    });
}
