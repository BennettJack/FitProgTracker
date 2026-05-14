import { useState } from "react";
import { api } from "../../api/apiClient";

export default function AccessTests() {
  const [message, setMessage] = useState<string>("");

  const checkUserRole = async () => {
    try {
      const response = await api.get("/UserAccount/testUserRole");

      setMessage(
        `User role access granted: ${response.data.username ?? response.data.Username}`,
      );
    } catch (error: any) {
      setMessage(
        `User role access denied: ${error.response?.status ?? "unknown"}`,
      );
    }
  };

  const checkSuperUserRole = async () => {
    try {
      const response = await api.get("/UserAccount/testSuperUserRole");

      setMessage(
        `SuperUser role access granted: ${response.data.username ?? response.data.Username}`,
      );
    } catch (error: any) {
      setMessage(
        `SuperUser role access denied: ${error.response?.status ?? "unknown"}`,
      );
    }
  };

  const checkCookie = async () => {
    try {
      const response = await api.get("/UserAccount/checkcookie");

      setMessage(
        `Cookie check → Authenticated: ${response.data.authenticated}, Username: ${response.data.username}, Roles: ${response.data.roles?.join(", ")}`,
      );
    } catch (error: any) {
      setMessage(`Cookie check failed: ${error.response?.status ?? "unknown"}`);
    }
  };

  return (
    <div className="p-4 space-y-4">
      <div className="flex space-x-4">
        <button onClick={checkUserRole}>Check User Role</button>
        <button onClick={checkSuperUserRole}>Check SuperUser Role</button>
        <button onClick={checkCookie}>Check Cookie</button>
      </div>

      {message && <p className="testText">{message}</p>}
    </div>
  );
}
