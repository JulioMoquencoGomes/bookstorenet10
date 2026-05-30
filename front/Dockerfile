FROM node:alpine
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
# For Create React App or Vite dev servers
CMD ["npm", "start"]