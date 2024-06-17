ARG MONGO_VERSION
FROM mongo:${MONGO_VERSION}

RUN mkdir -p /data /log

VOLUME /data /log

EXPOSE 27017

CMD ["mongod", "--dbpath", "/data", "--logpath", "/log/mongod.log"]