//create new row
export async function createNewToken(data) {
    try {
      const response = await fetch("https://localhost:7088/api/Authentication/Authenticate", {
        method: "POST",
        body: JSON.stringify(data),
        headers: {
          "Content-type": "application/json; charset=UTF-8", 
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