# Use an official lightweight Ubuntu base image
FROM ubuntu:22.04

# Set the working directory inside the container
WORKDIR /app

# Install required system dependencies (including OpenGL support)
RUN apt-get update && apt-get install -y \
    python3-pip \
    python3-dev \
    libgl1-mesa-glx \
    libglib2.0-0 \
    && rm -rf /var/lib/apt/lists/*

# Copy the requirements file and install Python dependencies
COPY requirements.txt /app/
RUN pip install --no-cache-dir -r /app/requirements.txt

# Copy the entire application code into the container
COPY . /app/

# Expose port 8000 for FastAPI
EXPOSE 8000

# Start FastAPI using Gunicorn
CMD ["gunicorn", "-w", "4", "-k", "uvicorn.workers.UvicornWorker", "api.TestServer:app"]

