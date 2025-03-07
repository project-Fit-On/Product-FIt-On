# Use a lightweight Python image
FROM python:3.11

# Set environment variables
ENV PYTHONUNBUFFERED=1  # Ensures real-time logging

# Set the working directory
WORKDIR /app

# Copy the requirements file first (for better caching)
COPY api/requirements.txt .

# Install dependencies
RUN pip install --no-cache-dir -r requirements.txt

# Copy the rest of the application
COPY api/ .

# Expose the port FastAPI runs on
EXPOSE 8000

# Run FastAPI using Uvicorn
CMD ["uvicorn", "TestServer:app", "--host", "0.0.0.0", "--port", "8000"]