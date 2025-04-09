FROM nginx:alpine

# Limpa pasta padrão do Nginx
RUN rm -rf /usr/share/nginx/html/*

# Copia arquivos compilados Angular para o container
COPY dist/sakai-ng /usr/share/nginx/html

# Configurações personalizadas do Nginx para Angular
RUN echo $'server {\n\
    listen 80;\n\
    server_name localhost;\n\
    root /usr/share/nginx/html;\n\
    index index.html;\n\
    location / {\n\
        try_files $uri $uri/ /index.html;\n\
    }\n\
}' > /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]