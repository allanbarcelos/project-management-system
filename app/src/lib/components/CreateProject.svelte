<h1>Create New Project</h1>

<form>
  <div>
    <label for="id">Project ID:</label>
    <input id="id" type="text" />
  </div>

  <div>
    <label for="name">Project Name:</label>
    <input id="name" type="text" />
  </div>

  <div>
    <label for="description">Description:</label>
    <textarea id="description"></textarea>
  </div>

  <div>
    <label for="startDate">Start Date:</label>
    <input id="startDate" type="date" />
  </div>

  <div>
    <label for="endDate">End Date:</label>
    <input id="endDate" type="date" />
  </div>

  <div>
    <label for="status">Status:</label>
    <select id="status">
      <option value="NotStarted">Not Started</option>
      <option value="InProgress">In Progress</option>
      <option value="Completed">Completed</option>
    </select>
  </div>

  <button type="submit">Create Project</button>
</form>

<style>
  form {
    display: flex;
    flex-direction: column;
    gap: 1rem;
    max-width: 400px;
  }

  label {
    font-weight: bold;
  }

  input, textarea, select {
    width: 100%;
    padding: 0.5rem;
  }
</style>
