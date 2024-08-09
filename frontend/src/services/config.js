// config.js

export const setAPIToken = (token) => {
    sessionStorage.setItem('authToken', token);
  };
  
  export const getAPIToken = () => {
    return sessionStorage.getItem('authToken');
  };
  