# Use a lightweight Python image
FROM python:3.11

# Set the working directory
WORKDIR /app

# Copy requirements file
COPY requirements.txt .

# Install dependencies
RUN pip install --no-cache-dir -r requirements.txt

# Copy the rest of the application
COPY . .

# Expose the port FastAPI runs on
EXPOSE 8000

# Run FastAPI using Uvicorn
CMD ["uvicorn", "api.TestServer:app", "--host", "0.0.0.0", "--port", "8000"]