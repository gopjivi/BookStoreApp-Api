import { useState, useEffect } from "react";
import { getAPIToken } from './config';

export function useFetch(url) {
  const [data, setData] = useState(null);

  useEffect(() => {
    fetch(url, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${getAPIToken()}`, // Get the token from config
      },
    })
      .then((res) => {
        if (!res.ok) {
          throw new Error("Failed to fetch data");
        }
        return res.json();
      })
      .then((data) => setData(data))
      .catch((error) => {
        console.error("Error fetching data:", error);
        setData(null); // Set data to null in case of error
      });
  }, [url]);

  return [data];
}
