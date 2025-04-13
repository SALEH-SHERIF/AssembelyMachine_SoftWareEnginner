# 🛠️ Assembly Machine - Car Manufacturing System

This module is responsible for assembling cars using components previously generated and stored in the database. It strictly follows the **SOLID principles** and uses design patterns such as **Repository Pattern** and **Dependency Injection** for flexibility and testability.

---

## 📦 Responsibilities

- Validate availability of required components.
- Retrieve assembly instructions.
- Pass instructions to the external DLL for assembly.
- Create and save the final `Car` object into the database.

---

## 📁 File Structure

| Folder              | File/Class                  | Responsibility                                           |
|---------------------|-----------------------------|----------------------------------------------------------|
| `Application/Services/` | `CarAssemblerService.cs`     | Core logic to validate, fetch, assemble, and save cars.  |
| `Application/DTOs/`     | `CarAssemblyRequest.cs`     | Request model to hold UI input before assembling.        |
| `Interfaces/`           | `IDllAssembler.cs`          | Interface for DLL interaction.                           |
| `Infrastructure/`       | `DllAssembler.cs`           | Implementation that interacts with the DLL.              |
| `UI/`                   | `Form1.cs`                  | Captures user input and triggers assembly logic.         |

---

## 🧠 How It Works

1. **User Inputs:**
   - Enters count and type for: Wheels, Doors, Glass, Motor.

2. **On Button Click (`Assemble`):**
   - Check if enough components exist in DB (by type + subtype).
   - Retrieve all related instructions.
   - Call external `AssemblerDLL` to simulate the assembly.
   - If success, save a new `Car` object in the `Cars` table.

3. **If Not Enough Components:**
   - Show an appropriate warning message.

---

## 🔁 Dependencies

- `AssemblerDLL` → Contains `void Assemble(List<string> instructions)` to simulate physical assembly.
- `IComponentRepository` → Used to fetch components and instructions.
- `ICarRepository` → Used to persist the final car object.

---

## ✅ Example Flow

```csharp
var request = new CarAssemblyRequest {
    WheelCount = 4,
    WheelType = "Steel",
    DoorCount = 0,
    DoorType = "",
    GlassCount = 6,
    GlassType = "Thin",
    MotorCount = 1,
    MotorType = "10اHorse"
};

if (_assemblerService.TryAssembleCar(request, out var message)) {
    MessageBox.Show(message); // Success
} else {
    MessageBox.Show(message); // Failure (e.g. Not enough parts)
}

✅ Design Principles Used
Single Responsibility → Each class has one focused job.

Open/Closed → You can extend without modifying core logic.

Liskov Substitution → All interfaces follow proper substitutability.

Interface Segregation → Small interfaces for targeted use.

Dependency Inversion → DLL and database interactions via interfaces

🔗 Related
Component Generator Machine

AssemblerDLL dependency should be placed in AssemblerDLL/ directory and referenced properly in the project.
