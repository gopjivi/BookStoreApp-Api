import { getAPIToken } from './config';

//create new row
export async function createNewData(url, data) {
  try {
    const response = await fetch(url, {
      method: "POST",
      body: JSON.stringify(data),
      headers: {
        "Content-type": "application/json; charset=UTF-8",
        "Authorization": `Bearer ${getAPIToken()}`, // Get the token from config
      },
    });

    if (!response.ok) {
      // Handle HTTP errors
      const errorData = await response.json();
      throw new Error(errorData.message || "Something went wrong");
    }

    return await response.json();
  } catch (error) {
    // Handle network or other errors
    console.error("Error creating new data:", error);
    throw error; // Re-throw the error to be handled by the calling code
  }
} 

//get all data
export function getAllData(url) {
  return fetch(url, {
    method: "GET",
    credentials: "same-origin",
    headers: {
      "Content-Type": "application/json",
      "Authorization": `Bearer ${getAPIToken()}`, // Get the token from config
    },
  })
    .then((response) => {
      if (!response.ok) {
        const error = response.json();
        throw new Error(error.message || "Failed to get data");
      }
      return response.json();
    })
    .catch((error) => {
      console.error("Error getting data:", error);
      throw error;
    });
}

//update
export async function updateData(url, id, data) {
  try {
    const response = await fetch(`${url}/${id}`, {
      method: "PUT",
      body: JSON.stringify(data),
      headers: {
        "Content-type": "application/json; charset=UTF-8",
        "Authorization": `Bearer ${getAPIToken()}`, // Get the token from config
      },
    });

    if (!response.ok) {
      // Handle HTTP errors
      const errorData = await response.json();
      throw new Error(errorData.message || "Something went wrong");
    }
   
    return await response.json();
  } catch (error) {
    // Handle network or other errors
    console.error("Error updating data:", error);
    throw error; // Re-throw the error to be handled by the calling code
  }
}

//delete
export async function deleteData(url, id) {
  try {
    const response = await fetch(`${url}/${id}`, {
      method: "DELETE",
      headers: {
        "Authorization": `Bearer ${getAPIToken()}`, // Get the token from config
      },
    });

    if (!response.ok) {
      // Handle HTTP errors
      const errorData = await response.json();
      throw new Error(errorData.message || "Something went wrong");
    }

    return response.status;
  } catch (error) {
    // Handle network or other errors
    console.error("Error deleting  data:", error);
    throw error; // Re-throw the error to be handled by the calling code
  }
}

//get data by id
export function getDataById(url, id) {
  return fetch(`${url}/${id}`, {
    method: "GET",
    credentials: "same-origin",
    headers: {
      "Content-Type": "application/json",
      "Authorization": `Bearer ${getAPIToken()}`, // Get the token from config
    },
  })
    .then((response) => {
      if (!response.ok) {
        const error = response.json();
        throw new Error(error.message || "Failed to get data");
      }
      return response.json();
    })
    .catch((error) => {
      console.error("Error getting data:", error);
      throw error;
    });
}
