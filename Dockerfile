# Use the official Blender image as the base
FROM blender:latest

# Set the working directory inside the container
WORKDIR /app

# Install Python and dependencies
RUN apt-get update && apt-get install -y python3-pip \
    && rm -rf /var/lib/apt/lists/*

# Copy the FastAPI dependencies
COPY requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

# Copy the entire FastAPI application code
COPY . .

# Expose port 8000 for the FastAPI server
EXPOSE 8000

# Start the FastAPI app using Gunicorn and Uvicorn
CMD ["gunicorn", "-w", "4", "-k", "uvicorn.workers.UvicornWorker", "TestServer:app"]
